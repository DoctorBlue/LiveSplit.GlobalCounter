# LiveSplit.GlobalCounter
A Simple counter component for LiveSplit, modified from the [forked LiveSplit.Counter source](https://github.com/LiveSplit/LiveSplit.Counter) to keep the values between `0` and `9` (inclusive), for global tracking purposes in *The Legend of Zelda*.

## [Click here to download the latest release of this component!](https://github.com/DoctorBlue/LiveSplit.GlobalCounter/releases/latest/download/LiveSplit.GlobalCounter.dll)
- Once downloaded, drop it into the `Components` directory of your installation LiveSplit.

## Additional Features
- In addition to the simple counting feature, the following are also available to enjoy:
  - Use your number pad keys (digits 1-9) to advance the counter by the amount selected
  - Display the next set of items to drop next to the counter number (e.g. `(1 B 1 ♥) 0`)
    - `1` and `5` represent rupees
    - `♥` represents Heart
    - `B` represents Bomb
    - `C` represents Clock
    - `F` represents Fairy

## Packages / Requirements for Development

- Built against .NET Framework 4.6.1, so you'll need build support for that within your development environment.
- All required DLLs and references from LiveSplit should be available within [`libs/LiveSplit`](https://github.com/DoctorBlue/LiveSplit.GlobalCounter/tree/main/libs/LiveSplit). 
  - The version of LiveSplit these DLLs come from is recorded in the subdirectory path within `libs`.
  - If the component ceases to work after a LiveSplit update, update these references and try to build against those.
