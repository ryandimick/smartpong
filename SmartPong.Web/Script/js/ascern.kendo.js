//Disable and enable buttons in the toolbar
function enableToolbarBtns(e) {
	$('.k-grid-' + e).removeClass('k-state-disabled');
}

function disableToolbarBtns(e) {
	$('.k-grid-' + e).addClass('k-state-disabled');
}

//Filter for textboxes
function onFilterKeyDown(e) {
    e.element.css("width", "90%").addClass("k-textbox").keydown(function (e) {
        setTimeout(function () {

            var k = e.which;

            if (k === 20 /* Caps lock */
             || k == 16 /* Shift */
             || k == 9 /* Tab */
             || k == 27 /* Escape Key */
             || k == 17 /* Control Key */
             || k == 91 /* Windows Command Key */
             || k == 19 /* Pause Break */
             || k == 18 /* Alt Key */
             || k == 93 /* Right Click Point Key */
             || (k >= 35 && k <= 40) /* Home, End, Arrow Keys */
             || k == 45 /* Insert Key */
             || (k >= 33 && k <= 34) /*Page Down, Page Up */
             || (k >= 112 && k <= 123) /* F1 - F12 */
             || (k >= 144 && k <= 145)) { /* Num Lock, Scroll Lock */
                return false;
            }
            else {
                $(e.target).trigger("change");
            }
        });
    });
}

//Success and Error Confirmation
function SuccessNotification(msg) {
    var popupNotification = $("#notification").data("kendoNotification");
    popupNotification.show({
        message: msg
    },
        "upload-success");
}

function ErrorNotification(msg) {
    var popupNotificationError = $("#notification").data("kendoNotification");
    popupNotificationError.show({
        title: "Error",
        message: msg
    },
        "error");
}

//Validation
function ValidateForm() {
    var container = $("#editingForm");
    kendo.init(container);
    container.kendoValidator({
        rules: {
            validmask: function (input) {
                if (input.is("[data-validmask-msg]") && input.val() !== "") {
                    var maskedtextbox = input.data("kendoMaskedTextBox");
                    return maskedtextbox.value().indexOf(maskedtextbox.options.promptChar) === -1;
                }
                return true;
            }
        }
    });
}

//Shared Kendo Modal
function openAndCloseModal(e) {
    var wnd = $("#modal").data("kendoWindow");
    if (e === 1) {
        wnd.center().open();
        return null;
    }
    wnd.close();
    return null;
}
//Shared Kendo Modal

//Delete Modal
function openDeleteModal(e) {
    var wnd = $("#DeleteModal").data("kendoWindow");
    $('#DeleteModal').parent().addClass("DeleteModal");
    if (e === 1) {
        wnd.center().open();
        return null;
    }
    wnd.center().close();
    return null;
}

function deleteRecords() {
    if (!$('#delete').hasClass("k-state-disabled")) {
        openDeleteModal(1);
    }
}

function noButton() {
    openDeleteModal();
    return false;
}
//Delete Modal

//Dirty Modal
function modal_Cancel() {
    if (isDirty) {
        document.getElementById('yesDirty').onclick = function () { yesSaveButton(); }
        openCloseDirtyModal(1);
        return false;
    }
    openAndCloseModal();
    return false;
}

function openCloseDirtyModal(e) {
    var dirty = $("#DirtyModal").data("kendoWindow");
    $('#DirtyModal').parent().addClass("DirtyModal");
    if (e === 1) {
        dirty.center().open();
        return null;
    }
    dirty.close();
    return null;
}

function noSaveButton() {
    openCloseDirtyModal();
}

function yesSaveButton() {
    openCloseDirtyModal();
    openAndCloseModal();
    return false;
}

function onSelectCheckDirty(e) {
    if (dirty > 0) {
        if (isDirty) {
            e.preventDefault();
            document.getElementById('yesDirty').onclick = function () { onDirtyEvent($(e.item).index()); }
            document.getElementById("dirtyMessage").innerHTML = 'There are some unsaved changes. Are you sure you want to continue?';
            return openCloseDirtyModal(1);
        }

        var idx = $(e.item).index();
        if (isBitSet(dirty, idx)) {

            isDirty = true;
        }
        return false;
    }
    return false;
}

function onDirtyEvent(selectedIndex) {
    var tabStrip = $("#ModalTabStrip").data("kendoTabStrip");
    isDirty = false;
    tabStrip.select(selectedIndex);
    return openCloseDirtyModal();
}

function powerOfTwo(i) {
    return Math.pow(2, i);
}

function isBitSet(number, bit) {
    return ((number >> bit) % 2 !== 0);
}

function setBit(number, bit) {
    return number | 1 << bit;
}

function clearBit(number, bit) {
    return number & ~(1 << bit);
}
//Dirty Modal

//Spin JS
var opts = {
    lines: 13 // The number of lines to draw
, length: 28 // The length of each line
, width: 14 // The line thickness
, radius: 42 // The radius of the inner circle
, scale: .70 // Scales overall size of the spinner
, corners: 1 // Corner roundness (0..1)
, color: '#000' // #rgb or #rrggbb or array of colors
, opacity: 0.25 // Opacity of the lines
, rotate: 0 // The rotation offset
, direction: 1 // 1: clockwise, -1: counterclockwise
, speed: 1 // Rounds per second
, trail: 60 // Afterglow percentage
, fps: 20 // Frames per second when using setTimeout() as a fallback for CSS
, zIndex: 2e9 // The z-index (defaults to 2000000000)
, className: 'spinner' // The CSS class to assign to the spinner
, top: '50%' // Top position relative to parent
, left: '50%' // Left position relative to parent
, shadow: false // Whether to render a shadow
, hwaccel: false // Whether to use hardware acceleration
, position: 'absolute' // Element positioning
}

function renderSpinner(targetName) {
    var spinner = new Spinner(opts).spin(targetName);
    spinner.spin(targetName);
}
///////

//textBoxWathcer

function textBoxWatcher(textBoxName) {
    $(document)
        .on('input',
            '#' + textBoxName,
            function () {
                var txtValue = document.getElementById(textBoxName);

                if (txtValue.defaultValue !== txtValue.value) {
                    dirty = setBit(dirty, detailsTab);

                    return isDirty = true;
                }
                dirty = clearBit(dirty, detailsTab);
                return isDirty = false;
            });
}

function selectedGrid_dataBound() {
    var view = this.dataSource.view();
    for (var i = 0; i < view.length; i++) {
        if (view[i].Selected) {
            this.tbody.find("tr[data-uid='" + view[i].uid + "']")
                .addClass("k-state-selected")
                .find(".checkbox")
                .attr("checked", "checked");
        }
    }
}

//Yes button for largeModals
function SaveLargeModalButton() {
    openCloseDirtyModal();
    openAndCloseLargeModal();
    return false;
}

//function onDataBound() {
//    configureButtons();
//}

//function onChange() {
//    configureButtons();
//}

//function onRefresh() {
//    this.center();
//}

//function onCreate(cUrl, title) {
//    var modal = $('#modal').data("kendoWindow");
//    modal.title(title);
//    modal.refresh({ url: cUrl });
//    modal.center().open();
//}
///