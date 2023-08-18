using System;
using System.Windows.Forms;

namespace AG.COM.SDM.GeoDataBase
{
    public delegate void ObjectAddedEventHandler(object sender,EventArgs e);
    public delegate void ObjectCheckedEventHandler(object sender);
    public delegate void ObjectSelectedEventHandler(object sender, TreeViewEventArgs e);
    public delegate void ObjectDeletedEventHandler(object sender,EventArgs e);
    public delegate void ObjectRefreshedEventHandler(object sender,EventArgs e);
    public delegate void RefreshAllEventHandler(object sender,EventArgs e);
}
