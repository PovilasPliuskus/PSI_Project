function handleLogIn() {
    var email = document.getElementById("InputEmail").value;
    var password = document.getElementById("InputPassword").value;

    var userData = {
        email: email,
        password: password
    };

    // Make a POST request to the server to validate the login
    fetch("/Users/Login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(userData)
    })
        .then(response => {
            if (response.ok) {
                console.log("Login successful");
                window.location.href = "/Notes/NotePage";
            } else {
                // The login failed
                console.log("Login failed");
            }
        })
        .catch(error => {
            console.error("Error during login:", error);
        });
}