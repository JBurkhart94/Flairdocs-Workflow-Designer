var steps = 0;

$(document).ready(function () {
    $('#add-step-button').click(addStep);
});

function addStep(e) {
    steps += 1;
    var code = '<div class="workflow-step"></div >';
    $(code).insertBefore('#add-step-button');
    var tag = $('#workflow-designer-window-edit');
    tag.scrollLeft(350 * steps);
}