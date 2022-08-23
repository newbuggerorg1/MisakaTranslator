# MisakaTranslator 御坂翻译器

    This a special branch where the 'MisakaTranslator' is designed for the new version of 'Windows', 
    also here is a experimental plot for new features.

    这是为适配新版Windows而特别设立的分支，并且是新功能的试验田。

## Reason

The original branch of 'MisakaTranslator' support 'Windows 7', but 'Windows 7' has published since Oct 22 2009, so a lot of leatest features can't run properly. Yet Some old game can't execute in leatest 'Windows', and we don't want to make 'MisakaTranslator' become a BigMac. As a result, We create this branch to rebuild step by step.

MisakaTranslator一直以来支持Windows 7，但是Windows 7已经发布十余年，众多新特性无法在Windows 7上正确执行。可是极少数老古董在新Windows上会因兼容性而崩溃，同时我们不希望MisakaTranslator为适配新系统同时兼顾旧系统而出现更多问题，甚至成为巨无霸。因此设立该分支而逐步重构。

## Roadmap

    If the following features can execute properly in 'Windows 7', 
    we may cherry-pick these back to original branch.

    如果新功能兼容Windows 7，可能会迁移回已有分支。

- [x] Update build toolchain
  - [ ] 🏃 Hybrid App Development (C++ & C#, as Modern C++ as possible)
- [x] Use VC-LTL without installing Visual C++ Redistributable
- [x] Update target framework to .Net Framework 4.8.1 and Microsoft C++ 14.3
- [x] Package with AppX/MSIX for the modern experience of deployment
- [x] 💪 Support Per-Monitor DPI-Aware (Need more test)
- [x] 💪 Support x64 and arm64 (Need more test)
- [x] 💪 Support Windows built-in OCR
- [ ] 🏃 Use more built-in components of the new Windows and .Net
  - [x] Sqlite, Json, Font Icons
  - [ ] Active Code Page, On-Screen Handwriting Keyboard...
- [ ] 🏃 Migrate settings ini files to json
- [ ] 🏃 Rebuild a modern UI
- [ ] 🏃 Split 3rd-party components
  - [ ] 🏃 Mecab
  - [ ] Textractor
- [ ] Remove obsolete components
  - [x] Tesseract4
- [ ] Support games form Steam
- [ ] Extract texts by Speech Recognition (like Live captions)
- [ ] ..., including some suggestions in issues (Welcome to contribute, including Pull Request)

## Others
As same as [ReadMe.md](https://github.com/hanmin0822/MisakaTranslator/tree/master#readme) in original branch.

详见原始分支[ReadMe.md](https://github.com/hanmin0822/MisakaTranslator/tree/master#readme)。

    Thanks for your support. 感谢您的支持。
