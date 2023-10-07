document.getElementById("addNoteButton").addEventListener("click", function () {
    console.log("Sveikas!");
    fetch("/ProjectPages/CreateNote")
        .then(response => response.json())
        .then(data => {
            const button = createButton(data);
            const textareaContainer = document.getElementById("opened-notes-div");
            replaceTextHeader(textareaContainer, data);
            replaceTextarea(textareaContainer, data);

            document.getElementById("listOfNotes").appendChild(button);

            button.addEventListener("click", function () {
                console.log("Button clicked:", data);
            });
        })
        .catch(error => {
            console.error("Error:", error);
        });
});

function createButton(data) {
    const button = document.createElement("button");
    button.id = data.id;
    button.addEventListener("click", function () {
        replaceTextHeader(document.getElementById("opened-notes-div"), data);
        replaceTextarea(document.getElementById("opened-notes-div"), data);
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

function createTextarea(data) {
    const textarea = document.createElement("textarea");
    textarea.classList.add("note-textarea");

    // Add an event listener to update the 'value' property of 'data' on textarea change
    textarea.addEventListener("input", function () {
        data.value = textarea.value;
    });

    return textarea;
}

function createTextHeader(data) {
    const textHeader = document.createElement("input");
    textHeader.classList.add("note-text-header");
    textHeader.type = "text";

    textHeader.addEventListener("input", function () {
        data.name = textHeader.value;
    });

    return textHeader;
}

function replaceTextarea(container, data) {
    const existingTextarea = container.querySelector(".note-textarea");
    if (existingTextarea) {
        container.removeChild(existingTextarea);
    }
    const newTextarea = createTextarea(data); // Pass the 'data' object
    newTextarea.textContent = data.value;
    container.appendChild(newTextarea);
}

function replaceTextHeader(container, data) {
    const existingTextHeader = container.querySelector(".note-text-header");
    if (existingTextHeader) {
        container.removeChild(existingTextHeader);
    }
    const newTextHeader = createTextHeader(data); // Pass the 'data' object
    newTextHeader.value = data.name;
    container.appendChild(newTextHeader);
}

function setNoteButtonContent(notesNameSpan, notesValueSpan, data) {
    notesNameSpan.textContent = data.name;
    notesValueSpan.textContent = data.value;
}
