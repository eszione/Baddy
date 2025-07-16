# baddy

# Pre-requisites
- Java/JDK 11 and above
- Android SDK (Android studio or Visual Studio has the setup)
- NDK

# Distributing to Google Play
- Update android version code in AndroidManifest.xml as we cannot reuse the code once it's in Google Play
- Archive for publishing (Visual Studio/Jetbrains Rider) - ensure you are in Release Mode
- Sign APK (can create a new certificate or import an existing one) - badcrew123 is the password
- Create release in GooglePlay and upload APK/AAB

# MacOS setup
- Install Java/JDK (brew install --cask temurin) - target the correct version of temurin
