document.getElementById("sortByNameAsc").addEventListener("click", function () {

    deleteChildElements();

    const sortOption = "nameAsc";
    const data = {
        sortOption: sortOption
    };

    fetch("/Notes/SortNotes?sortOption=" + sortOption, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            noteList.push(...data);
            noteList.forEach(function (note) {

                const button = createButton(note);
                const textareaContainer = document.getElementById("opened-notes-div");
                replaceTextHeader(textareaContainer, note);
                replaceTextarea(textareaContainer, note);

                document.getElementById("listOfNotes").appendChild(button);
            });
        })
        .catch(error => {
            console.error("Error loading notes to server:", error);
        });
});


document.getElementById("sortByNameDesc").addEventListener("click", function () {

    deleteChildElements();

    const sortOption = "nameDesc";
    const data = {
        sortOption: sortOption
    };

    fetch("/ProjectPages/SortNotes?sortOption=" + sortOption, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            noteList.push(...data);
            noteList.forEach(function (note) {

                const button = createButton(note);
                const textareaContainer = document.getElementById("opened-notes-div");
                replaceTextHeader(textareaContainer, note);
                replaceTextarea(textareaContainer, note);

                document.getElementById("listOfNotes").appendChild(button);
            });
        })
        .catch(error => {
            console.error("Error loading notes to server:", error);
        });
});

document.getElementById("sortByDateAsc").addEventListener("click", function () {

    deleteChildElements();

    const sortOption = "creationDateAsc";
    const data = {
        sortOption: sortOption
    };

    fetch("/ProjectPages/SortNotes?sortOption=" + sortOption, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            noteList.push(...data);
            noteList.forEach(function (note) {

                const button = createButton(note);
                const textareaContainer = document.getElementById("opened-notes-div");
                replaceTextHeader(textareaContainer, note);
                replaceTextarea(textareaContainer, note);

                document.getElementById("listOfNotes").appendChild(button);
            });
        })
        .catch(error => {
            console.error("Error loading notes to server:", error);
        });
});

document.getElementById("sortByDateDesc").addEventListener("click", function () {

    deleteChildElements();

    const sortOption = "creationDateDesc";
    const data = {
        sortOption: sortOption
    };

    fetch("/ProjectPages/SortNotes?sortOption=" + sortOption, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            noteList.push(...data);
            noteList.forEach(function (note) {

                const button = createButton(note);
                const textareaContainer = document.getElementById("opened-notes-div");
                replaceTextHeader(textareaContainer, note);
                replaceTextarea(textareaContainer, note);

                document.getElementById("listOfNotes").appendChild(button);
            });
        })
        .catch(error => {
            console.error("Error loading notes to server:", error);
        });
});

function deleteChildElements() {

    printNoteList();

    noteList.splice(0);
    // Get references to the parent elements by their IDs
    const listOfNotes = document.getElementById("listOfNotes");
    const openedNotesDiv = document.getElementById("opened-notes-div");

    // Remove all child elements from listOfNotes
    while (listOfNotes.firstChild) {
        listOfNotes.removeChild(listOfNotes.firstChild);
    }

    // Remove all child elements from openedNotesDiv
    while (openedNotesDiv.firstChild) {
        openedNotesDiv.removeChild(openedNotesDiv.firstChild);
    }
}