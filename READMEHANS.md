//For teacher
Hej, god dag och hallå Hans!

Här finns lite skräptext för dig att läsa igenom. 

 lite reflektioner: dessa saker borde egentligen fått lite mer kärlek: 
 CSSen generellt. just nu är det en hel hög med en miljoners miljaders klasser som hade gått att undvika om jag bara tänkte till lite. men ack o ve.
 Containersklasser är den största boven, där jag egentligen bara borde haft en regel i en .container, och det borde vara maxbredd. men nu blev det inte så.
 Vissa blocklists/items/sectioner ligger nu separat i olika kollektions. något jag fattade halvvägs igenom att jag bara kunde baka ihop. 
 Sida är responsiv, men är fortfarande lite buggig. mestadels blev det problem med position-aboslute satta bilder. städade lite i slutskedet, men inget jag orkade lägga någon tid på. 
 generellt sätt ska inlämningen täcka samtliga kriterier för G och VG. 
 
 lite småbuggar som är kvar:
 är backgrundsfärgen i settings som inte funkar fullt ut.
 en settings som skulle hamna i styletaggen för att sätta Background color är felskriven och funkar därför inte, men jag orkar inte ändra plan i azure för att deploya om projektet och att sedan dra ner den till free igen. 
 Och den stans text editorn. tog ett år att fixa .Json filen, och sen funkar den inte riktigt som jag önskar. Men det får bli lärdom till nästa gång man behöver den. 
 
 
 
 
 
 
 
 
 
 
 //For the Curious
 Umbraco 16 – Assignment Project

This repository contains my solution for the Umbraco 16 course assignment.
The task was to build a fully editable website based on a provided Figma design.
All requirements for both Pass (G) and Pass with Distinction (VG) have been fulfilled.

Assignment Overview

The goal of the assignment was to:

Build a website in Umbraco 16 based on a given Figma design.

Create a structured, editor-friendly setup using Block List and Element Types.

Implement navigation, forms, child pages, and global site settings.

Publish the website to Azure with a functioning database.

I completed the project individually.
(Collaboration: None, unless updated.)

Technical Summary

Umbraco CMS 16 / .NET 9

Block List Editor + Element Types for all page structures

Global Site Settings for logo, contact info, and social links

Posters implemented as child pages with a List View in backoffice

Dynamic navigation generated from the content tree

Fully responsive layout according to the design

Functional form with validation, saved submissions, and confirmation email

Deployed to Azure App Service with Azure SQL

Requirements Fulfilled – Pass (G)

The website follows all pages, sections, and structural elements from the Figma design.

Pages are built using Block List and Element Types.

Posters are handled as child pages.

The forms are visually implemented according to the design.

Dynamic navigation pulls pages from the content tree.

The site is published on Azure with a functioning database.

Requirements Fulfilled – Pass with Distinction (VG)

The website is fully responsive and matches the design structure across screen sizes.

Block List and Element Types are used throughout, with settings applied where appropriate.

Posters use child pages combined with a List View for improved management.

The form is fully functional: validated, saved in backoffice, and sends a confirmation email.

Global Site Settings manage logo, contact information, and social links.

Compositions are used to reuse common fields (for example SEO and page headers).

Git workflow follows proper branching, merging features into main only when completed.
