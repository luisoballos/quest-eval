# quest-eval

This repository contains the source code and documentation for a Virtual Reality (VR) application developed as part of my MSc in Biomedical Engineering. The application is designed to assess the Quality of Upper Extremity Skills Test (QUEST), used to evaluate upper limb motor function in patients with muscle function loss, such as those suffering from cerebral palsy, presented at CEU San Pablo University, Madrid, Spain.
 
## Features
- VR-based QUEST evaluation: Digitalizes 18 key items of the QUEST test using Oculus Quest 2 and Unity.
- Hand Gesture Recognition: Uses real-time hand tracking and gesture recognition for accurate assessments.
- 3D Game Environment: Simulates the traditional QUEST environment with virtual objects like cubes, pencils, and small cereal grains.
- Automated Data Storage: Saves patient evaluation scores and precision measurements to files for further analysis.

## Technologies used
- Unity: For VR development and game design.
- Oculus Integration SDK: For hand tracking and VR interactions.
- C#: For scripting and implementation of gesture recognition algorithms.
- Visual Studio: Integrated development environment (IDE) for coding and debugging.
- Oculus Quest 2: VR headset for motion tracking and immersive interaction.

## How it works
- The application prompts users to perform specific hand gestures from the QUEST test.
- Hand movement data is captured and processed using Unity’s hand tracking system.
- The system compares the real-time gesture to pre-defined expected gestures, providing a score based on accuracy.
- Results, including precision measurements of each gesture, are saved in a log file for review.


## Results
During testing, the application achieved:
- 5 mm accuracy in gesture recognition.
- Precision of 5±1.4 mm in real-time hand tracking, ensuring reliable assessments.
