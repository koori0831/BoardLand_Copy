using UnityEngine;

public abstract class MVPUIManagerBase<T, V> : MonoBehaviour where T : IUIEvent<V>
{
    [SerializeField] ViewBase<T, V> view;
    protected PresenterBase<T, V> presenter;
    protected ModelBase<T, V> model;

    protected virtual void Awake()
    {
        model.Initialize();
        presenter.Initialize(view, model);
    }

    protected virtual void OnDisable()
    {
        model?.Dispose();
        presenter?.Dispose();
    }
}
