//Register event listeners when the document loads
$(document).ready(function () {
    $('#add-step-button').click(addStep);
});

//Function to add a step to the workflow design window
function addStep() {
    var code = '<div class="workflow-step"></div >';
    $(code).insertBefore('#add-step-button');
    $('#workflow-designer-window-edit').scrollLeft($('workflow-designer-window-edit').width());
}