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
    console.log("testing");
}

$('#create-workflow-button').click(function () {
    swal({
        title: 'Create New WorkFlow',
        input: 'text',
        inputPlaceholder: 'Inpur Your Workflow Name Here ',
        // type: 'info',
        showCancelButton: true,
        confirmButtonColor: ' #2db300',
        cancelButtonColor: 'red',
        confirmButtonText: 'Create',
        inputValidator: function (value) {
            return new Promise (function(resolve, reject){
                if (value) {
                    resolve()
                } else {
                    reject("Workflow Name cannot be empty! ")
                }
            }
        )}
    }).then(function () {

        resetWorkflow();
        swal(
            'created', 'Your Workflow is created successfully!', 'success'
        )
    })
});

function resetWorkflow(){
    console.log("Resetting workflow");
    $("#workflow-designer-window-edit").html('<div id="add-step-button" class="glyphicon glyphicon-chevron-right"></div>');
    $('#add-step-button').click(addStep);
    addStep();
    steps = 1;
}