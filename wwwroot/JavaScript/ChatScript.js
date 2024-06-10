
function AddMessageToViewWithImageId(message, ElementClassNameMess, ProfileSrc, ElementClassNameImg, MessageId) {
    var MessageImgContainer = document.createElement("div");
    var ImgContainer = document.createElement("div");
    var MessContainer = document.createElement("div");
    var MessageContainerChild = document.createElement("div");

    MessageContainerChild.className = "MessageContainerChild" + ElementClassNameMess;
    MessageImgContainer.className = ElementClassNameMess + "Container";

    var MessageArea = document.getElementById("MessageConatiner");
    MessageArea.appendChild(MessageImgContainer);
    MessageImgContainer.appendChild(MessageContainerChild);
    var Message = document.createElement("div");
    Message.innerHTML = message;
    Message.className = "Message";
    MessContainer.className = ElementClassNameMess;

    MessageContainerChild.appendChild(MessContainer);
    MessageContainerChild.appendChild(ImgContainer);

    var imgElement = document.createElement("img");
    imgElement.src = ProfileSrc;
    imgElement.id = MessageId;
    ImgContainer.className = ElementClassNameImg;
    imgElement.className = "photo";


    MessContainer.appendChild(Message);
    ImgContainer.appendChild(imgElement);

    scrollToBottom();
}
function AddMessageToViewWithImageURL(message, ElementClassNameMess, Url, ElementClassNameImg, MessageId) {
    var MessageImgContainer = document.createElement("div");
    var ImgContainer = document.createElement("div");
    var MessContainer = document.createElement("div");
    var MessageContainerChild = document.createElement("div");

    MessageContainerChild.className = "MessageContainerChild" + ElementClassNameMess;
    MessageImgContainer.className = ElementClassNameMess + "Container";

    var MessageArea = document.getElementById("MessageConatiner");
    MessageArea.appendChild(MessageImgContainer);
    MessageImgContainer.appendChild(MessageContainerChild);
    var Message = document.createElement("div");
    Message.innerHTML = message;
    Message.className = "Message";
    MessContainer.className = ElementClassNameMess;

    var DeleteIcon = document.createElement("img");
    DeleteIcon.className = "DeleteIcon";
    DeleteIcon.src = "/Icons/delete.png";
    DeleteIcon.role = "button";
    DeleteIcon.onclick = function () { DeleteMessage(MessageId, MessageImgContainer) };

    MessageContainerChild.appendChild(MessContainer);
    MessageContainerChild.appendChild(ImgContainer);

    var imgElement = document.createElement("img");
    imgElement.src = Url;
    imgElement.id = MessageId;
    ImgContainer.className = ElementClassNameImg;
    imgElement.className = "photo";

    MessContainer.appendChild(Message);
    MessContainer.appendChild(DeleteIcon);
    ImgContainer.appendChild(imgElement);

    scrollToBottom();
}
function scrollToBottom() {
    var div = document.getElementById("MessageConatiner");
    div.scrollTop = div.scrollHeight;
}

function DeleteMessage(MessageId, MessageObj) {
    fetch(`/DeleteMessage/${MessageId}`);
    MessageObj.remove();
}
function ChangePhoto(PhotoId) {
    var photo = document.getElementById(PhotoId);
    photo.src = "/Icons/message-read.png";
}