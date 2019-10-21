#!/bin/sh
# Pre-install script for appveyor: install build deps
# (c) Reuben Thomas 2019.
# This file is in the public domain.

# Convert symlink files to actual links
# From https://stackoverflow.com/questions/5917249/git-symlinks-in-windows
git config --global alias.rm-symlinks '!'"$(cat <<'ETX'
__git_rm_symlinks() {
  case "$1" in (-h)
    printf 'usage: git rm-symlinks [symlink] [symlink] [...]\n'
    return 0
  esac
  ppid=$$
  case $# in
    (0) git ls-files -s | grep -E '^120000' | cut -f2 ;;
    (*) printf '%s\n' "$@" ;;
  esac | while IFS= read -r symlink; do
    case "$symlink" in
      (*/*) symdir=${symlink%/*} ;;
      (*) symdir=. ;;
    esac

    git checkout -- "$symlink"
    src="${symdir}/$(cat "$symlink")"

    posix_to_dos_sed='s_^/\([A-Za-z]\)_\1:_;s_/_\\\\_g'
    doslnk=$(printf '%s\n' "$symlink" | sed "$posix_to_dos_sed")
    dossrc=$(printf '%s\n' "$src" | sed "$posix_to_dos_sed")

    if [ -f "$src" ]; then
      rm -f "$symlink"
      fsutil hardlink create "$doslnk" "$dossrc"
    elif [ -d "$src" ]; then
      rm -f "$symlink"
      cmd //C mklink //J "$doslnk" "$dossrc"
    else
      printf 'error: git-rm-symlink: Not a valid source\n' >&2
      printf '%s =/=> %s  (%s =/=> %s)...\n' \
          "$symlink" "$src" "$doslnk" "$dossrc" >&2
      false
    fi || printf 'ESC[%d]: %d\n' "$ppid" "$?"

    git update-index --assume-unchanged "$symlink"
  done | awk '
    BEGIN { status_code = 0 }
    /^ESC\['"$ppid"'\]: / { status_code = $2 ; next }
    { print }
    END { exit status_code }
  '
}
__git_rm_symlinks
ETX
)"

git rm-symlinks
