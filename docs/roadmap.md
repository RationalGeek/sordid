Sordid Roadmap
======
### Alpha release
* ~~Set up alpha website, deployment scripts~~
* ~~Set up continuous integration, deployment in TeamCity~~
* ~~Create "how you can help" documentation and dev mailing list~~
* ~~User registration has to include collecting email~~
* ~~Enable code-first migrations without automatic migrations, disable automatic data loss~~
* ~~Fix the issue with TeamCity / GitHub / Azure timeouts~~
* ~~About page with attributions for libraries, fonts, etc., and especially to Jim Butcher and Evil Hat~~
* ~~About page should link back to Github project~~
* ~~Add license file to Github project, About page~~
* ~~Fix issue where character is dirty when it loads~~
* ~~Help should appear next to image thumbnail to suggest image upload sites~~
* ~~Help page, with at least an FAQ section~~
* ~~Intro on home page, with big red warning about this being alpha~~
* Finish print page
  * Finish the Misc Stats panel
  * Needs to include character portrait - but where?
  * ~~Needs to size bottom two panels to line up at bottom~~
* Type in all the reference data: stock powers, templates, 
* Add link to alpha site from GitHub project page
* Add Google Analytics
* Post to community sites about project and solicit help

### Beta release
* Get a real domain name
* Set up beta infrastructure, including SMTP server so system can send emails to users
* Stable, production-ready database
* Feature complete for DresdenRPG character creation, management, and printing
* Error tracking, logging, analytics all in place and functioning
* Tested and working in all modern browsers
  * Mechanism to hide header/footer in Chrome doesn't seem to work in Firefox
* Tested and working (at least the important parts) on mobile browsers
* Uservoice (or equivalent) for feature requests
* Allow for changing of username, email
* Emails should be enforced unique
* User's email validated by sending them a confirmation email
* Welcome email sent to new users
* Support external logins (Facebook, etc.)
* Proper functioning of back button on character manage page

### Production release 1.0
* Capturing of special equipment such as magic items, potions, armor
* Capture description of powers and show them in dialog
* Support for admin users that can impersonate other users
* Set up production infrastructure
* Create some unit tests

### Beyond 1.0
* App understands templates and can autoselect powers and what-not
* Print out aspect details sheet
* Share characters with other users
* Assemble characters into a campaign and let GM print out summary sheets
* Make the app smarter so that it can double-check the character is legit and give warnings
* Spellbook feature that helps build spells
* Quicker NPC creation for GMs
* City creation and tracking
* Support for other Fate games beside DresdenRPG
