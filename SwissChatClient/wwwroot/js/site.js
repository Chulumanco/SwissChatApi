
let swalresponse = false;
let modalAddContact = document.getElementById('btnSave')
modalAddContact.addEventListener('click', e => {
    e.preventDefault();
    let user = $("#contact_username").val();
    console.log(user);
    saveContact(user);
   
   
});




const saveContact = (username) => {
    const url = "/Contact/Create/?username=" + username
    fetch(url, {
            method: 'POST',
        headers: {
            "Content-Type": "application/json",
          },
          
        })
            .then(response => response.json())
            .then((data) => {
              
            
                displaySwal(swalresponse, data.message);
            })
            .catch((err) => {
                displaySwal(swalresponse, err.message);
            })
};

document.getElementById("sendButton").addEventListener("click", function () {
   
    let username = $(this).data('username');
    getUsername(username);
  
});



const getUsername = (username) => {
    const url = "/Contact/SendMessage/?username=" + username
    fetch(url, {
        method: 'GET',
        headers: {
            "Content-Type": "application/json",
        },
    })
        .then(response => response.json())
        .then((data) => {


            displaySwal(swalresponse, data.message);
        })
        .catch((err) => {
            displaySwal(swalresponse, err.message);
        })
};

//const appendUsername = (username) => {
//    let htmlResponse = username;
//    let divElement = document.createElement('div');
//    divElement.innerHTML = htmlResponse;
//    let containerElement = document.getElementById('header');
//    containerElement.appendChild(divElement);
//}

