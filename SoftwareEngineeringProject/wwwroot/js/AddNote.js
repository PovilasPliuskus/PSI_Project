const noteList = [];

document.getElementById("addNoteButton").addEventListener("click", function () {
    fetch("/ProjectPages/CreateNote")
        .then(response => response.json())
        .then(data => {
            // Add the received Note object to the noteList
            noteList.push(data);
            //printNoteList();

            const button = createButton(data);
            const textareaContainer = document.getElementById("opened-notes-div");
            replaceTextHeader(textareaContainer, data);
            replaceTextarea(textareaContainer, data);

            document.getElementById("listOfNotes").appendChild(button);

            /*button.addEventListener("click", function () {
                console.log("Button clicked:", data);
            });*/
        })
        .catch(error => {
            console.error("Error:", error);
        });
});

function createButton(data) {
    const button = document.createElement("button");
    button.id = data.id;

    const notesNameSpan = document.createElement("span");
    const notesValueSpan = document.createElement("span");
    button.appendChild(notesNameSpan);
    button.appendChild(notesValueSpan);
    button.classList.add("note-button");
    notesNameSpan.classList.add("notes-name-span");

    const textareaContainer = document.getElementById("opened-notes-div");

    const textarea = createTextarea(data, notesNameSpan, notesValueSpan);
    const textHeader = createTextHeader(data, notesNameSpan, notesValueSpan);

    setNoteButtonContent(notesNameSpan, notesValueSpan, data);

    button.addEventListener("click", function () {
        replaceTextHeader(textareaContainer, data);
        replaceTextarea(textareaContainer, data, notesNameSpan, notesValueSpan);
    });

    return button;
}

function createTextarea(data, notesNameSpan, notesValueSpan) {
    const textarea = document.createElement("textarea");
    textarea.classList.add("note-textarea");

    // Add an event listener to update the 'value' property of 'data' on textarea change
    textarea.addEventListener("input", function () {
        data.value = textarea.value;
        setNoteButtonContent(notesNameSpan, notesValueSpan, data);
    });

    return textarea;
}

function createTextHeader(data, notesNameSpan, notesValueSpan) {
    const textHeader = document.createElement("input");
    textHeader.classList.add("note-text-header");
    textHeader.type = "text";

    textHeader.addEventListener("input", function () {
        data.name = textHeader.value;
        setNoteButtonContent(notesNameSpan, notesValueSpan, data);
    });

    return textHeader;
}

function replaceTextarea(container, data, notesNameSpan, notesValueSpan) {
    const existingTextarea = container.querySelector(".note-textarea");
    if (existingTextarea) {
        container.removeChild(existingTextarea);
    }
    const newTextarea = createTextarea(data, notesNameSpan, notesValueSpan);
    newTextarea.textContent = data.value;
    container.appendChild(newTextarea);
}

function replaceTextHeader(container, data, notesNameSpan, notesValueSpan) {
    const existingTextHeader = container.querySelector(".note-text-header");
    if (existingTextHeader) {
        container.removeChild(existingTextHeader);
    }
    const newTextHeader = createTextHeader(data, notesNameSpan, notesValueSpan);
    newTextHeader.value = data.name;
    container.appendChild(newTextHeader);
}

function setNoteButtonContent(notesNameSpan, notesValueSpan, data) {
    if (notesNameSpan && notesValueSpan) {
        notesNameSpan.textContent = data.name;
        notesValueSpan.textContent = data.value;
    }
}


function printNoteList() {
    noteList.forEach(function (note) {
        console.log("Note Creation Date: " + note.informationRecord.creationDate);
        console.log("Note Information ID: " + note.informationRecord.id);
        console.log("Note Name: " + note.name);
        console.log("Note Value: " + note.value);
        console.log("Note Rows: " + note.rows);
        console.log("Note Columns: " + note.columns);
        console.log("Note Category: " + note.category);


        console.log("\n"); // Add a newline for separation
    });
}