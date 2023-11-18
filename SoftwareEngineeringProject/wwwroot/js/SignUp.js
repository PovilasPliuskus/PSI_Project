function handleSubmit() {
    // Get input values
    var firstName = document.getElementById("FirstName").value;
    var lastName = document.getElementById("LastName").value;
    var email = document.getElementById("InputEmail").value;
    var password = document.getElementById("InputPassword").value;

    // Create an object with the input values
    var userData = {
        firstName: firstName,
        lastName: lastName,
        email: email,
        password: password
    };

    console.log("User Data:", userData);

    // Send the userData to the server using a fetch POST request
    fetch("/Users/CreateUser", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(userData)
    })
        .then(response => {
            if (response.ok) {
                console.log("User created successfully.");
            } else {
                console.error("Error creating user:", response.statusText);
            }
        })
        .catch(error => {
            console.error("Error creating user:", error);
        });
}