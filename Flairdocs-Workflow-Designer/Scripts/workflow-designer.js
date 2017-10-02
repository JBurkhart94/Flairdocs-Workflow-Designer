var steps = 0;

//Register event listeners when the document loads
$(document).ready(function () {
    $('#add-step-button').click(addStep);
});

//Function to add a step to the workflow design window
function addStep(e) {
    steps += 1;
    console.log("Adding Step");
    var code = '<div class="workflow-step"></div >';
    $(code).insertBefore('#add-step-button');
    var tag = $('#workflow-designer-window-edit');
    tag.scrollLeft(350 * steps);
}