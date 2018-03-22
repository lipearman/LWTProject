/// <reference path="jquery-3.1.1.intellisense.js" />
/// <reference path="jquery-3.1.1.js" />
(function($, dxbsDemo) {
    var search = {
        searchTimeout: null,
        lastSearch: null,
        selectedItem: null,
        isInCallback: false,

        onSearchBoxGotFocus: function() {
            if(search.listenerTimeout)
                clearTimeout(search.listenerTimeout);
            search.listenerTimeout = setInterval(function() {
                var text = searchEditor.GetValue();
                if(search.lastText !== text) {
                    search.lastText = text;
                    search.doSearch(text);
                }
            }, 100);
        },
        onSearchBoxLostFocus: function() {
            if(search.listenerTimeout)
                clearTimeout(search.listenerTimeout);
            search.listenerTimeout = null;
        },


        doSearch: function(text) {
            search.selectedItem = null;
            if(text && text.length > 2) {
                searchResults.PerformCallback(text);
                search.setContainerVisiblity(true);
            }
            else
                search.setContainerVisiblity(false);
        },
        onSearchPopupBeginCallback: function() {
            search.isInCallback = true;
        },
        onSearchPopupEndCallback: function() {
            search.isInCallback = false;
        },
        setContainerVisiblity: function(visible) {
            if(visible)
                $(searchResults.GetMainElement()).slideDown();
            else
                $(searchResults.GetMainElement()).slideUp();
        }
    };
    dxbsDemo.search = search;
})(jQuery, window.dxbsDemo || (window.dxbsDemo = {}));