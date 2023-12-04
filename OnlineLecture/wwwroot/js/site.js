// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*function registerClass(idSubject) {
    fetch('Home/AddTheClass', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
           
        },
        body: JSON.stringify({ IdSubject: idSubject })
    })
        .then(response => response.json())
        .then(data => {
            // Xử lý kết quả nếu cần
            console.log(data);
            window.location.href = "/Home/Index"; // Chuyển hướng sau khi gửi thành công
        })
        .catch(error => {
            console.error('Error:', error);
        });
}*/

var handleDelete = () => {
    const modalDelete = document.getElementById("deleteModal");
    const btnDeletes = document.querySelectorAll(".btn-delete");
    const btnDeleteConfirm = document.querySelector(".btn-delete-confirm");
    const btnCanel = document.querySelector(".btn-cancel");
    const overlay = document.querySelector(".overlay");

    btnDeletes.forEach((item) => {
        item.onclick = () => {
            modalDelete.classList.add("show");
            overlay.classList.add("show");
            const postId = item.getAttribute("data-post-id");
            btnDeleteConfirm.addEventListener("click", () => {
                try {
                    var xhr = new XMLHttpRequest();
                    xhr.open("DELETE", `/Lecture/Delete/${postId}`, true);
                    xhr.setRequestHeader("Content-Type", "application/json");
                    xhr.onreadystatechange = () => {
                        if (xhr.readyState === XMLHttpRequest.DONE) {
                            if (xhr.readyState === 4 && xhr.status === 200) {
                                console.log("success");
                                handleLoad();
                            } else {
                                console.log("Error deleting data.");
                            }
                        }
                    };
                    xhr.send();
                } catch (error) {
                    console.log(error);
                }
            });

            btnCanel.onclick = () => {
                modalDelete.classList.remove("show");
                overlay.classList.remove("show");
            };
        };
    });
};

var handleUpdate = () => {
    const updateModal = document.getElementById("updateModal");
    const btnUpdates = document.querySelectorAll(".btn-update");
    const btnUpdateConfirm = document.querySelector(".btn-update-confirm");
    const overlay = document.querySelector(".overlay");
    const btnClose = document.querySelector(".btn-huy");
    var nameLecture = document.querySelector(".input-content");
    var description = document.querySelector(".input-description");
    var fileLecture = document.querySelector(".input-image");
   /* var postDate = document.querySelector(".input-date");*/

    btnUpdates.forEach((item) => {
        item.onclick = () => {
            updateModal.classList.add("show");
            overlay.classList.add("show");
            var postId = item.getAttribute("data-post-id");
            console.log(postId, 'update.........')
            try {
                var xhr = new XMLHttpRequest();
                xhr.open("GET", `/Lecture/GetLectureById?idLecture=${postId}`, true);
                xhr.setRequestHeader("Content-Type", "application/json");
                xhr.onreadystatechange = () => {
                    if (xhr.readyState === XMLHttpRequest.DONE) {
                        if (xhr.readyState === 4 && xhr.status === 200) {
                            const res = JSON.parse(xhr.responseText);
                            console.log(res)
                            nameLecture.value = res.nameLecture;
                            description.value = res.description;
                            console.log(res.postId, 'GetLectureById.....');

                            btnUpdateConfirm.addEventListener("click", () => {
                                try {
                                    var formData = new FormData();

                                    formData.append("postId", res.postId);
                                    formData.append("nameLecture", nameLecture.value);
                                    formData.append("description", description.value);
                                    formData.append("fileLecture", fileLecture.files[0]);
                            

                                    $.ajax({
                                        url: "/Lecture/Update/",
                                        type: "PUT",
                                        data: formData,
                                        processData: false,
                                        contentType: false,
                                        success: function (result) {
                                            console.log(result);
                                            handleLoad();
                                        },
                                        error: function (error) {
                                            console.error(error);
                                        },
                                    });
                                } catch (error) {
                                    console.log(error);
                                }
                            });
                        } else {
                            console.log("Error getting data.");
                        }
                    }
                };
                xhr.send();
            } catch (error) {
                console.log(error);
            }

            btnClose.onclick = () => {
                updateModal.classList.remove("show");
                overlay.classList.remove("show");
            };
        };
    });
};

var handleLoad = () => {
    try {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", "/Lecture/GetAllLecture", true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onreadystatechange = () => {
            if (xhr.readyState === 4 && xhr.status === 200) {
                var posts = JSON.parse(xhr.responseText);
                var postListHtml = `<table class="table table-striped textTable">
                    <thead class="thead-dark">
                        <tr>
                            <th style="width: 50px">
                               STT
                            </th>
                            <th style="width: 350px">
                               Name Lecture
                            </th>
                            <th style="width: 300px">
                               Description
                            </th>
                            <th style="width: 200px">
                                File
                            </th>
                          
                            <th style="width: 200px">
                               Action
                            </th>
                            
                         </tr>
                    </thead>
                    <tbody>
                `;

                posts.forEach((post, index) => {
                    postListHtml += `
                        <tr>
                            <td>
                               ${index + 1} 
                            </td>
                            <td style="text-align:left">
                               ${post.nameLecture} 
                            </td>
                            <td style="text-align:left">
                                ${post.description} 
                            </td>
                            <td>
                                <a href="${post.fileLecture}" >File ${post.nameLecture}</a>
                            </td>
                            <td>
                                <div class="btn-group d-flex justify-content-center">
                                    <a data-post-id=${post.idLecture} class="btn btn-success btn-update mr-2">Update</a> 
                                    <a data-post-id=${post.idLecture} class="btn btn-delete btn-danger">Delete</a>
                                    
                                </div>
                              
                            </td>
                        </tr>

                        <div class="overlay"></div>

                         <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                              <div class="modal-dialog">
                                <div class="modal-content">
                                  <div class="modal-header">
                                    <h4 class="modal-title">Are you sure?</h4>
                                  </div>
                                  <div class="modal-body">
                                    <p>Do you really want to delete these records? This process cannot be undone.</p>
                                  </div>
                                  <div class="modal-footer">
                                    <button type="button" class="btn btn-default btn-cancel" data-dismiss="modal">Close</button>
                                    <button class="btn btn-danger btn-delete-confirm">Delete</button>
                                  </div>
                                </div>
                              </div>
                          </div>


                          <div class="modal fade" id="updateModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                              <div class="modal-dialog">
                                  <div class="modal-content">
                                      <div class="modal-header">
                                          <h5 class="modal-title" id="exampleModalLabel">Edit</h5>
                                      </div>
                                      <div class="modal-body">
                                          <form>
                                              <div class="mb-3">
                                                  <label for="recipient-name" class="col-form-label">Name Lecture</label>
                                                  <input type="text" class="form-control input-content">
                                              </div>
                                              <div class="mb-3">
                                                  <label for="message-text" class="col-form-label">Description</label>
                                                  <input type="text" class="form-control input-description">
                                              </div>
                                              <div class="mb-3">
                                                  <label for="message-text" class="col-form-label">File</label>
                                                  <input type="file" class="form-control input-image">
                                              </div>
                                           
                                               
                                          </form>
                                      </div>
                                      <div class="modal-footer">  
                                          <button type="button" class="btn btn-secondary btn-huy" data-bs-dismiss="modal">Close</button>
                                          <button type="button" class="btn btn-primary btn-update-confirm">Update</button>
                                      </div>
                                      </div>
                              </div>
                          </div>
                    `;
                });
                postListHtml += "</tbody>" + "</table>";
                var postList = document.getElementById("result");
                postList.innerHTML = postListHtml;
                handleDelete();
                handleUpdate();
            } else if (xhr.readyState === 4) {
                alert("Failed to get post list.");
            }
        };
        xhr.send();
    } catch (error) {
        console.log(error);
    }
};

window.onload = () => {
    handleLoad();
    handleSearchPost();
};

var handleSearchPost = () => {
    const searchInput = document.querySelector(".search");
    let debounceTimeout;

    searchInput.addEventListener("input", function () {
        clearTimeout(debounceTimeout);
        debounceTimeout = setTimeout(function () {
            const searchTerm = searchInput.value.trim();
            if (searchTerm == "") {
                handleLoad();
            } else {
                searchPost(searchTerm);
            }
        }, 500);
    });
};

var searchPost = (name) => {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", `/Admin/Search?name=${name}`, true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = () => {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var posts = JSON.parse(xhr.responseText);
            var postListHtml = `<table class="table table-striped">
                    <thead class="thead-dark">
                        <tr>
                            <th style="width: 50px">
                               STT
                            </th>
                            <th style="width: 350px">
                               Content
                            </th>
                            <th style="width: 250px">
                               Description
                            </th>
                            <th style="width: 300px">
                                PostImageURL
                            </th>
                            <th style="width: 150px">
                               PostDate
                            </th>
                            <th style="width: 200px">
                               Action
                            </th>
                            
                         </tr>
                    </thead>
                    <tbody>
                  `;

            posts.forEach((post, index) => {
                postListHtml += `
                        <tr>
                            <td>
                               ${index + 1} 
                            </td>
                            <td style="text-align:left">
                               ${post.content} 
                            </td>
                            <td style="text-align:left">
                                ${post.description} 
                            </td>
                            <td>
                                <img style="height:60px;width:80px;" src="/Uploads/${post.postImageURL
                    }" />
                            </td>
                            <td>
                                ${post.postDate} 
                            </td>
                            <td>
                                <button class="btn btn-update text-lg-right"><i class="bi bi-pencil-square"></i></button> |
                                <button  data-post-id=${post.postId
                    }   data-bs-toggle="modal" data-bs-target="#deleteModal" class="btn btn-delete text-lg-right"><i class="bi bi-trash"></i></button>
                            </td>
                        </tr>


                        <div class="overlay"></div>

                        <div class="modal" id="deleteModal">
                            <div class="modal-dialog">
                              <div class="modal-content">
                                <div class="modal-header">
                                  <h4 class="modal-title">Are you sure?</h4>
                                </div>
                                <div class="modal-body">
                                  <p>Do you really want to delete these records? This process cannot be undone.</p>
                                </div>
                                <div class="modal-footer">
                                  <button type="button" class="btn btn-default btn-cancel" data-dismiss="modal">Close</button>
                                  <button class="btn btn-danger btn-delete-confirm">Delete</button>
                                </div>
                              </div>[]
                            </div>
                        </div>
                    `;
            });
            postListHtml += "</tbody>" + "</table>";
            var postList = document.getElementById("result");
            postList.innerHTML = postListHtml;
            handleDelete();
        } else if (xhr.readyState === 4) {
            alert("Failed to get post list.");
        }
    };
    xhr.send();
};
