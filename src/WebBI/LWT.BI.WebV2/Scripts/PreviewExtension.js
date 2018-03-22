// Creates and implements a custom PreviewDashboardExtension class.

function PreviewDashboardExtension(_wrapper) {
    var _this = this;
    this._wrapper = _wrapper;
    this._control = _wrapper.GetDashboardControl();
    this._toolbox = this._control.findExtension('toolbox');
    this.name = "dxdde-Preview-dashboard";
    this.PreviewDashboard = function () {
        if (_this.isExtensionAvailable()) {
            var dashboardid = _this._control.dashboardContainer().id;
            //var param = JSON.stringify({ DashboardID: dashboardid, ExtensionName: _this.name });
            _this._toolbox.menuVisible(false);
            //_this._wrapper.PerformDataCallback(param, function () {
                //_this._control.close();
            //});
            //alert(cbPreview);
            cbPreview.PerformCallback(dashboardid);
        }
    }
    this._menuItem = {
        id: this.name,
        title: "Preview",
        click: this.PreviewDashboard,
        selected: ko.observable(false),
        disabled: ko.computed(function () { return !_this._control.dashboard(); }),
        index: 113,
        hasSeparator: true,
        data: _this
    };
}

PreviewDashboardExtension.prototype.isExtensionAvailable = function () {
    return this._toolbox !== undefined;
}

PreviewDashboardExtension.prototype.start = function () {
    if (this.isExtensionAvailable())
        this._toolbox.menuItems.push(this._menuItem);
};
PreviewDashboardExtension.prototype.stop = function () {
    if (this.isExtensionAvailable())
        this._toolbox.menuItems.remove(this._menuItem);
};
