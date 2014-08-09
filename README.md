Animated GIF Viewer
======
![](https://bytebucket.org/jessezhong/animatedgifviewer/raw/d0d7e38d1b0dbaa90b79ebc5d47a149f433aaa0f/Screenshots/ScreenshotWithTheme.JPG)

Table of Contents
-----------------
1. What is the Animated GIF Viewer?
2. Why?
3. Download & Installation
4. Current Features
5. Building from the Source
6. Documentation

1. What is the Animated GIF Viewer?
-------------------------------
**Animated GIF Viewer** is a small program that can open animated GIF images and play them. It functions as an image viewer, supporting image types such as JPEGs, BMPs, PNGs, ICOs, and TIFFs. The viewer draws heavy influence from the standard Windows Photo Viewer that is shipped with Windows OS, but features notable differences.

Animated GIF Viewer is a free program that is distributed under the GNU General Public License. See [LICENSE.txt](https://bitbucket.org/jessezhong/animatedgifviewer/src/59dc56699bb402382f55e4cdce5632173237d35c/LICENSE.txt?at=master).

2. Why?
-------
As much as I love the lean and minimalist Windows Photo Viewer, it lacks a few key features that I enjoy:

- Reading and playing animated GIFs.
- Dragging and dropping images into the window to open them.
- View images you added to the folder you are viewing without having to reopen the Viewer.
- Open and read ZIP and other popular archive files without having to extract them.
- Intuitive keyboard shortcuts for things like "Slideshow" mode.
- Preference window for customizing certain aspects of the Viewer, such as zoom rate.
- A reskinnable user interface.

This project aims to address these desires.

3. Download & Installation
--------------------------
The most recent version is **1.0.5328**, which can be downloaded here: [AnimatedGIFViewer-v1.0.5328.zip](https://bitbucket.org/jessezhong/animatedgifviewer/downloads/AnimatedGIFViewer-v1.0.5328.zip)

New and old releases of the viewer are available on the [Downloads](https://bitbucket.org/jessezhong/animatedgifviewer/downloads) page.

Make sure your system has **Microsoft .NET Framework 4.5** installed and updated. If you keep your system fully updated, you should be fine.

Otherwise, you can download and install the latest version of the framework here:

[http://www.microsoft.com/en-us/download/details.aspx?id=30653](http://www.microsoft.com/en-us/download/details.aspx?id=30653)

To use the program, extract it from the ZIP to any folder of your choosing. 

You can drag and drop the image you wish to view on the **Animated GIF Viewer** icon or open the program by double clicking the icon and clicking **File>Open** in the menubar and searching for your image.

4. Current Features
-----------
The following is an list of features exclusive to the **Animated GIF Viewer**:

- Opens and displays GIF, JPEG/JPG, BMP/DIB, PNG, ICO, and TIFF images.
	- Allows **dragging and dropping** of images into the window to open the image in the program.
	- Allows **dragging and dropping** of images over the executable (EXE) to open the program with the image.
- Plays GIF images that have animated frames.
	- Supports animations that are faster than the rate supported by Windows (ex: Internet Explorer).
	- Supports animations with variable frame delays.
	- Supports animations that have masking frames.
- Allows **zooming in or out** (magnification) of an image via **mouse wheel** and **"magnifying glass" button**.
	- Allows animations to be played even when zoomed in or out.
- Allows **image rotation**, for both clockwise and counter clockwise.
- Allows for **deleting** and **copying** of an image from within the viewer.
	- Copies retain undoctored information from their originals. (Important for animated GIFs)
- Allows **cycling and viewing images in the same folder** using the **"previous" and "next" arrow buttons**.
	- Knows when images are new images are added and includes them in the list of view-able images. (Not possible with Windows Photo Viewer)
- Allows for **full screen mode** where images are automatically stretched to the size of the screen for easy viewing.
- Changes **appearance** with the system's **Windows Theme**.

5. Building from the Source
---------------------------
These are the prerequisites that you will need to have downloaded and installed before you can build the source:

- Microsoft Visual Studio 2012
	- You can download the free Express version here: [http://www.microsoft.com/en-us/download/details.aspx?id=34673](http://www.microsoft.com/en-us/download/details.aspx?id=34673)
- Git (optional) ( [http://git-scm.com/downloads](http://git-scm.com/downloads) ) or SmartGit/Hg (optional) ( [http://www.syntevo.com/smartgit/](http://www.syntevo.com/smartgit/) )

To obtain the source you can download the repository from the [Downloads](https://bitbucket.org/jessezhong/animatedgifviewer/downloads) page. Here's a direct link: [Download repository](https://bitbucket.org/jessezhong/animatedgifviewer/get/9476dfddbec2.zip).

Alternatively, you may clone the repository using Git or a SCM client, such as SmartGit/Hg.

Once you have obtained the source, **open** the **Project Solution** (AnimatedGifViewer.sln) with Visual Studio. Press **F7** or, from the menu bar, go to and click **Build > Build Solution**. 

When the project is finished compiling, you can find the resulting assembly / executable in the folder **AnimatedGifViewer / bin / Debug** or **AnimatedGifViewer / bin / Release** depending on which mode you built the project in, **Debug** or **Release**.

The actual program's executable  is called **Animated GIF Viewer.exe**. You may move it anywhere you'd like and use it.

6. Documentation
----------------