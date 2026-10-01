using UnityEngine;

public abstract class MVPUIManagerBase<TEvent, TModelData> : MonoBehaviour where TEvent : IUIEvent<TModelData>
{
    [SerializeField] private ViewBase view;
    protected PresenterBase<TEvent, TModelData> presenter;
    protected ModelBase<TEvent, TModelData> model;

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
