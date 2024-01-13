#!/bin/sh
echo -ne '\033c\033]0;Project-TD\a'
base_path="$(dirname "$(realpath "$0")")"
"$base_path/Test.x86_64" "$@"
