# PH Shared Element

A demo of an approach to achieve [shared element navigation](https://developer.android.com/training/material/animations.html#Transitions) using MvvmCross 5 and 6.

Link to original [Stack Overflow question](https://stackoverflow.com/questions/43804827/is-there-a-xamarin-mvvmcross-android-shared-element-navigation-example) which includes simplified approach.

---

This repository contains three samples

 - __MvxCachingFragmentCompatActivity__ - A custom implementation demonstrating an approach to achieve shared element transitions. The sample makes use of a list that navigates to either an Activity or a Fragment showing how to achieve each type of transition. This approach supports MvvmCross version `5.0.0` to `5.1.1`.
 - __Attribute Presentation__ - A custom implementation demonstrating an approach using the updated Android presenters introduced in MvvmCross `5.2.0`. This approach supports MvvmCross version `5.2.0` and greater.
 - __Official Support__ - A sample implementation demonstrating an approach for MvvmCross version `6.0.0` and greater.

---

![Share Element Demo](/screenshots/share_element.gif "Transition in action")

