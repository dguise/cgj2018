# cgj2018
>Castle Game Jam is a rapid result driven game development event where teams create prototypes over the course of 1 week in a big castle

## Our submission
tbd
### Themes
 - Design: 
 - Graphics: 
 - Sound: 
 - Code: 

### Technologies
Unity, 2D

## Event
### Schedule
![Schedule](https://scontent-arn2-1.xx.fbcdn.net/v/t1.0-9/35859352_2121858461431323_6825207241029713920_n.png?_nc_cat=0&oh=52110bb88ef7f23bde533568c1ab183e&oe=5BDD1A7E)
### Theme 

![Theme](https://scontent-arn2-1.xx.fbcdn.net/v/t1.0-9/36469852_2133804110236758_6297372300270370816_n.png?_nc_cat=0&oh=45de243d594824cec891aac6b611d3bf&oe=5BE1853E)



#### How to rebind Input?
To rebind a key for all controllers, you should change the file InputTemplate in ProjectSettings, "ProjectSettings/InputTemplate". TEMP should be used in place of a setting that should change depending on the controllerId.
After changes have been made, just run the script InputCreator.sh, "ProjectSettings/InputCreator.sh", while inside the ProjectSettings folder.

    ./InputCreator.sh

If you need to change access type just do:

    chmod u+x InputCreator.sh

to give the file execute rights.

The script will create and replace the InputManagement.asset file in your
folder.


To change individual bindings for individual controllers is a bit harder, but
could be made if you know the correct JoystickControllerId. Then change the
correct ID in InputManagement.asset

