var steps = 0;
swal.setDefaults(
    {
        width: '30%',
        progressSteps: ['1', '2'],
        confirmButtonColor: '#2db300',
        cancelButtonColor: 'red',
        
    }

);

var q = [
    {
        showCancelButton: true,
        title: 'Workflow Title',
        input: 'text',
        confirmButtonText: 'Next',
        inputValidator: function (value) {
            return new Promise(function (resolve, reject) {
                if (value) {
                    resolve()
                } else {
                    reject("Workflow Name cannot be empty! ")
                }
            }
            )
        }
    },
    {
        showCancelButton: true,
        title: 'Workflow Description',
        input: 'textarea',
        confirmButtonText: 'Done',
        inputValidator: function (value) {
            return new Promise(function (resolve, reject) {
                if (value) {
                    resolve()
                } else {
                    reject("Workflow Descriptions cannot be empty! ")
                }
            }
            )
        }
    }
];


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

$('#create-workflow-button').click(
    function () {
        swal.queue(q).then(function () {
            resetWorkflow();
            swal.resetDefaults();
        swal(
            'created', 'Your Workflow is created successfully!', 'success'
        )
    });

    }
);

//$('#create-workflow-button').click(function () {
//    swal({
//        title: 'Create New WorkFlow',
//        input: 'text',
//        inputPlaceholder: 'Inpur Your Workflow Name Here ',
//        // type: 'info',
//        showCancelButton: true,
//        confirmButtonColor: ' #2db300',
//        cancelButtonColor: 'red',
//        confirmButtonText: 'Create',
//        inputValidator: function (value) {  
//            return new Promise(function (resolve, reject) {
//                if (value) {
//                    resolve()
//                } else {
//                    reject("Workflow Name cannot be empty! ")
//                }
//            }
//            )
//        }
//    }).then(function () {
//        resetWorkflow();
//        swal(
//            'created', 'Your Workflow is created successfully!', 'success'
//        )
//    })
//});

function resetWorkflow() {
    console.log("Resetting workflow");
    $("#workflow-designer-window-edit").html('<div id="add-step-button" class="glyphicon glyphicon-chevron-right"></div>');
    $('#add-step-button').click(addStep);
    addStep();
    steps = 1;
}