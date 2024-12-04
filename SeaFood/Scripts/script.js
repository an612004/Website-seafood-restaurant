function ChangeImage(UploadImage, previewImg)
{
    if (UploadImage.files && UploadImage.files[0])
    {
        var reader = new FileRender();
        reader.onload = function (e)
        {
            $(previewImg).attr('scr', e.target.result);
        }
        reader.readAsDataURL(UploadImage.files[0]);
    }
}