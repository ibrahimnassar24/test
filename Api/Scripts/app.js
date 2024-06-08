
var ViewModel = function () {
    var self = this;
    self.books = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.authors = ko.observableArray();
    self.shown = ko.observable();
    self.newBook = {
        Author: ko.observable(),
        Genre: ko.observable(),
        Price: ko.observable(),
        Title: ko.observable(),
        Year: ko.observable()
    };

    var booksUri = '/api/books/';
    var authorsUri = '/api/authors/';

    function fetchHelper(url, method = "GET", data = null) {

        return new Promise((resolved, rejected) => {
            fetch(url, {
                method,
                headers: {
                    "Content-Type": "Application/Json"
                },
                body: data ? JSON.stringify(data) : null
            })
                .then((temp) => {
                    if (temp.status == 204) {
                        resolved(temp);
                        return;
                    }
                    

                    temp.json()
                        .then((data) => {
                            
                            
                            resolved(data);
                        })
                })
        });
    }

    function getAllBooks() {

        var temp = fetchHelper(booksUri)
            .then((data) => {
                self.books(data);
            })

    }

    self.getBookDetail = function (item) {
        const temp = booksUri + item.Id;
        fetchHelper(temp)
            .then((data) => {
                self.detail(data);
                self.shown("details");
            })
    }

    function getAuthors() {
        fetchHelper(authorsUri)
            .then((data) => {
                self.authors(data);
            })
    }


    self.addBook = function (formElement) {
        var book = {
            AuthorId: self.newBook.Author().Id,
            Genre: self.newBook.Genre(),
            Price: self.newBook.Price(),
            Title: self.newBook.Title(),
            Year: self.newBook.Year()
        };


        fetchHelper( booksUri, "POST", book)
            .then((data) => {
                getAllBooks();
                self.shown("");
            })
    }

    self.deleteBook = (item) => {
        const temp = booksUri + item.Id;
        fetchHelper(temp, "DELETE")
            .then(() => {
                getAllBooks();
                self.shown("");
            })
    }

    self.updateBook = function (item) {
        item.AuthorId = item.Author.Id;
        const temp = booksUri + item.Id;
        console.log(item)
        fetchHelper(temp, "PUT", item)
            .then(() => {
                getAllBooks();
                self.shown("");
            })
    }

    // Fetch the initial data.
    getAllBooks();
    getAuthors();
};

ko.applyBindings(new ViewModel());