window.addEventListener('load', function (event) {
    document.getElementById('btCreate').addEventListener('click', onAddNewUserClicked);
});

function writeNewUserFeedback(feedback){
    document.getElementById('newUserFeedback').textContent = feedback;
}

function onAddNewUserClicked() {    
    var username = document.getElementById('newUsername').value;
    var password = document.getElementById('password').value;
    var repassword = document.getElementById('repassword').value;

    if (username === '') {
        writeNewUserFeedback("Username is required");
        return;
    }

    if (password != repassword) {        
        writeNewUserFeedback("Passwords don't match");
        return;
    }

    fetch('/account/signup', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: `username=${username}&password=${password}`
    }).then(response => {
        if (response.ok)
            writeNewUserFeedback(`User ${username} was created, you can now login`);
        else
            writeNewUserFeedback("Error creating new username, make sure you haven't used this username before");
    });
}