document.getElementById("saveNoteButton").addEventListener("click", function () {
    // Call the function to save notes to the server
    saveNotesToFile();
});


function saveNotesToFile() {
    fetch("/ProjectPages/SaveNotes", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(noteList) // Send the entire client-side list of notes
    })
        .then(response => {
            if (response.ok) {
                console.log("Notes saved to server successfully.");
            } else {
                console.error("Error saving notes to server:", response.statusText);
            }
        })
        .catch(error => {
            console.error("Error saving notes to server:", error);
        });
}