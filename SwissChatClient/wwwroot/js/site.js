

let modalAddContact = document.getElementById('btnSave')
//modalAddContact.addEventListener('shown.bs.modal', function () {
//})
modalAddContact.addEventListener('click', e => {
    e.preventDefault();
    let user = $("#contact_username").val();
    console.log(user);
    saveContact(user);
   
   
});

const saveContact = (username) => {
   /* let user = $("#contact_username").val();*/
    const url = "/Contact/Create/?username=' " + username
    $.ajax({
        type: "POST",
        url: url,
        headers: {
              "Content-Type": "application/json",
           },
       
        success: function (status) {
            console.log(status);


        },
        error: function () {

        }


    });

}
