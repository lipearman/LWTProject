function onMenuInit() {
    updateTextBlocks();
    var header = document.querySelector('.header');
    header.className += ' ' + ASPxClientUtils.GetCookie('DXCurrentAdaptiveTheme');
    header.style.height = document.querySelector('.dxm-ltr').offsetHeight + 'px';
}
window.addEventListener('resize', function() {
    updateTextBlocks();
});
window.addEventListener('DOMContentLoaded', function() {
    initContent();
});

function initContent() {
    var content = document.querySelector('.content');
    var children = [];

    var child = null;
    while(child = content.children[0]) {
        content.removeChild(child);
        children.push(child);
    }

    var elementCount = 0;
    var group = createGroup();

    for(var i = 0, element = null; element = children[i]; i++) {
        element.className = 'column';
        group.appendChild(element);
        elementCount++;
        if(elementCount > 2) {
            elementCount = 0;
            content.appendChild(group);
            group = createGroup();
        }
    }
}

function createGroup() {
    var element = document.createElement('DIV');
    element.className = 'group';
    return element;
}
function updateTextBlocks() {
    var texts = document.querySelectorAll('.text');
    for(var i = 0, element = null; element = texts[i]; i++) {
        element.className = element.className.replace('hide', '');
        element.style.top = element.parentNode.offsetHeight - element.offsetHeight + 'px';
    }
}