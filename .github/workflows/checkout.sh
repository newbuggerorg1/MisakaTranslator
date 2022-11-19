#!/bin/bash

set -o errexit
set -o pipefail
set -o nounset

# Set magic variables for current file & dir
__dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
__file="${__dir}/$(basename "${BASH_SOURCE[0]}")"
__base="$(basename ${__file} .sh)"

MSKNAME=misaka
COMMIT="7fac13a"

EXTPATH=.building/
RPCPATH=.github/.external/${MSKNAME}
MSKPATH=${EXTPATH}/${MSKNAME}

external() {
	mkdir -f ${MSKPATH} && git clone https://github.com/newbuggerorg1/MisakaTranslator ${MSKPATH}
	pushd ${MSKPATH} && git checkout --force ${COMMIT} && popd
	cp -rfT ${RPCPATH}/ ${MSKPATH}/
}

external
