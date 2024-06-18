function ChangeFollowButton(Value) {
    var button = document.getElementById("Follow-Button");
    if (button)
    {
        button.innerText = Value;
    }
}
function GenerateBIOForm(BIOContent,UserId) {
    var BIOContainer = document.getElementById("UserBio-Conatiner");
    var FormContainer = document.createElement("div");
    BIOContainer.appendChild(FormContainer);
    var ChangeBioButton = document.getElementById("ChangeBioButton");
    ChangeBio.hidden = false;
    var BioParagarph = document.getElementById("UserBio");
    BioParagarph.hidden = true;
    var InputBio = document.createElement("input");
    InputBio.type = "text";
    InputBio.textContent = BIOContent;
    InputBio.id = 'InputBio';

    var Button = document.createElement("button");
    Button.onclick = ChangeBio(UserId);
    Button.textContent = "Change Bio";
    FormContainer.append(InputBio);
    FormContainer.append(Button);
 
}
function ChangeBio(UserId) {
    var BioInput = document.getElementById("InputBio");
    if (BioInput != null)
    {
        var BioContent = BioInput.value; 
        var BioParagarph = document.getElementById("UserBio");
        BioParagarph.textContent = BioContent;
        fetch(`/Bio/ChangeBio?Bio=${BioContent}&UserId=${UserId}`);
        RemoveInupt();
    }
}
function RemoveInupt()
{
    var InputBio = document.getElementById("InputBio");
    InputBio.remove();
    var BioParagarph = document.getElementById("UserBio");
    BioParagarph.hidden = false;
}