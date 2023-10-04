document.getElementById("addNoteButton").addEventListener("click", function () {
    fetch("/ProjectPages/CreateNote")
        .then(response => response.json())
        .then(data => {
            const button = createButton(data);
            const textareaContainer = document.getElementById("opened-notes-div");
            replaceTextarea(textareaContainer, button.id); // Pass button.id as a parameter
            document.getElementById("listOfNotes").appendChild(button);
        })
        .catch(error => {
            console.error("Error:", error);
        });
});

function createButton(data) {
    const button = document.createElement("button");
    button.id = data.id;
    button.addEventListener("click", function () {
        replaceTextarea(document.getElementById("opened-notes-div"), button.id); // Call replaceTextarea when the button is clicked
    });
    const notesNameSpan = document.createElement("span");
    const notesValueSpan = document.createElement("span");
    button.appendChild(notesNameSpan);
    button.appendChild(notesValueSpan);
    button.classList.add("note-button");
    notesNameSpan.classList.add("notes-name-span");

    setNoteButtonContent(notesNameSpan, notesValueSpan, data);

    return button;
}

function createTextarea() {
    const textarea = document.createElement("textarea");
    textarea.classList.add("note-textarea");
    textarea.placeholder = "Hello World!";
    return textarea;
}

function replaceTextarea(container, buttonId) {
    const existingTextarea = container.querySelector(".note-textarea");
    if (existingTextarea) {
        container.removeChild(existingTextarea);
    }
    const newTextarea = createTextarea();
    newTextarea.placeholder = "Button ID: " + buttonId; // Set the placeholder with the button's ID
    container.appendChild(newTextarea);
}

function setNoteButtonContent(notesNameSpan, notesValueSpan, data) {
    notesNameSpan.textContent = data.name;
    notesValueSpan.textContent = data.value;
}
