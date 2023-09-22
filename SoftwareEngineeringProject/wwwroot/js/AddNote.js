// Add Note button click
document.getElementById("addNoteButton").addEventListener("click", function () {
    fetch("/ProjectPages/CreateNote")
        .then(response => response.json())
        .then(data => {

            const button = document.createElement("button");
            const notesNameSpan = document.createElement("span");
            const notesValueSpan = document.createElement("span");
            button.appendChild(notesNameSpan);
            button.appendChild(notesValueSpan);

            setNoteButtonContent(notesNameSpan, notesValueSpan, data);

            button.classList.add("note-button");
            notesNameSpan.classList.add("notes-name-span");

            document.getElementById("listOfNotes").appendChild(button);
        })
        .catch(error => {
            console.error("Error:", error);
        });
});

function setNoteButtonContent(notesNameSpan, notesValueSpan, data) {
    notesNameSpan.textContent = data.name;
    notesValueSpan.textContent = data.value;
}