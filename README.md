# unityloadimage
This is a simple plugin to load image sprite from URL and set in to game object. 
## feature
After the first time image load from URL is succes, it will save on local directory. So, the next load with same URL don't need Connection to establish download, it will load image from local directory instead.
## how to use
This line used to load image from certain ```string URL``` and set to ```GameObject targetGameObject```
```c#
string URL = "http://unity-chan.com/images/imgKohaku.jpg";
GameObject targetGameObject;
if (LoadImage.IsReady)
   LoadImage.loadSpriteToObject(URL, targetGameObject);
```
Code above use to load image from URL and set ```Sprite``` to target game object
