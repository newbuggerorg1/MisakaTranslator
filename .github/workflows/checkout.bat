set MSKNAME=misaka
set COMMIT="7fac13a"

set EXTPATH=.building\
set RPCPATH=.github\.external\%MSKNAME%
set MSKPATH=%EXTPATH%\%MSKNAME%

mkdir -f %MSKPATH% && git clone https://github.com/newbuggerorg1/MisakaTranslator %MSKPATH%
cd %MSKPATH% && git checkout --force ${COMMIT} && cd ..
cp -rfT %RPCPATH%\ %MSKPATH%\
