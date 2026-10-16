using Alchemy.Inspector;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class TestDataTableReader : MonoBehaviour
{
    [SerializeField] private string path;

    [Button]
    public List<TableRow> TestRead()
    {
        if (string.IsNullOrEmpty(path))
            return null;

        FileStream streamer = new FileStream(path, FileMode.Open, FileAccess.Read);
        List<TableRow> tableRows = new List<TableRow>();

        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(streamer))
        {
            // 모든 시트 로드
            DataTableCollection tables = reader.AsDataSet().Tables;
            DataTable sheet = tables[0];
            object typeName = sheet.Rows[0][0];
            Type type = Type.GetType(typeName.ToString());
            if (type == null)
            {
                Debug.LogError("테이블에서 타입 변환 실패");
                reader.Dispose();
                reader.Close();
                return null;
            }

            FieldInfo[] fields = type.GetFields()
                .OrderBy(f => f.MetadataToken) // 선언 순서대로 정렬
                .ToArray();

            for (int i = 3; i < sheet.Rows.Count; i++)
            {
                object[] args = new object[fields.Length];
                for (int j = 0; j < fields.Length; j++)
                {
                    object param = null;
                    // 필드 타입으로 강제 캐스팅 시도. 실패 시, 예외 발생 
                    if (fields[j].FieldType.IsEnum)
                    {
                        // Enum인 경우 Enum.Parse 쓰도록
                        param = Enum.Parse(fields[j].FieldType, sheet.Rows[i][j].ToString());
                    }
                    else if (fields[j].FieldType.IsInterface || fields[j].FieldType.IsAbstract)
                    {
                        // 인터페이스거나 추상인 경우에는 셀 값에 적혀있는 타입으로 새 객체를 생성하고 param에 넣어준다.
                        Type cellType = Type.GetType(sheet.Rows[i][j].ToString());
                        param = Activator.CreateInstance(cellType);
                    }
                    else // 기본 타입 변환
                        param = Convert.ChangeType(sheet.Rows[i][j], fields[j].FieldType);

                    args[j] = param;
                }

                // 매개 변수 넣고 생성. 매개 변수가 일치하는 생성자가 없으면 예외
                TableRow tableRowInstance = (TableRow)Activator.CreateInstance(type, args);
                if (tableRowInstance == null)
                {
                    Debug.LogError($"타입이 TableRow를 상속하지 않음. 타입: {type}");
                    reader.Dispose();
                    reader.Close();
                    return null;
                }
                tableRows.Add(tableRowInstance);
            }

            reader.Dispose();
            reader.Close();

            return tableRows;
        }
    }
}
