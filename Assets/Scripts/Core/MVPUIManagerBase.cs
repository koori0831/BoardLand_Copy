using UnityEngine;

public abstract class MVPUIManagerBase<T, V> : MonoBehaviour where T : IUIEvent<V>
{
    [SerializeField] private ViewBase view;
    protected PresenterBase<T, V> presenter;
    protected ModelBase<T, V> model;

    protected virtual void OnEnable()
    {
        model.Initialize();
        presenter.Initialize(view, model);
    }

    protected virtual void OnDisable()
    {
        presenter?.Dispose();
        model?.Dispose();
    }
}
