namespace AG.COM.SDM.GeoDataBase
{
    public interface IGxTreeViewEvents
    {
        event ObjectAddedEventHandler ObjectAdded;
        event ObjectCheckedEventHandler ObjectChecked;
        event ObjectDeletedEventHandler ObjectDeleted;
        event ObjectRefreshedEventHandler ObjectRefreshed;
        event ObjectSelectedEventHandler ObjectSelected;
        event RefreshAllEventHandler RefreshAll;
    }
}
