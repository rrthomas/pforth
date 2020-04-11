#!/bin/sh
# Pre-install script for appveyor
# (c) Reuben Thomas 2019-2020.
# This file is in the public domain.

# Convert symlinks to duplicate files
# Adapted from https://stackoverflow.com/questions/5917249/git-symlinks-in-windows

# It is tricky to convert multi-level symlinks: changing the
# files on Windows does not change the git permissions, so git ls-files
# still shows the files as symlinks after they have been converted to plain files.
# Hence, simply run git rm-symlinks a few times, and ignore apparently
# non-existent files (because the contents of what we think are link files
# are now in fact the actual file contents).

# Directory links must in general remain links (because they can be linked
# into their own subdirectories), so do not attempt to cope with multi-level
# directory symlinks.
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
      cp "$dossrc" "$doslnk"
    elif [ -d "$src" ]; then
      rm -f "$symlink"
      cmd //C mklink //J "$doslnk" "$dossrc"
    fi

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
git rm-symlinks
git rm-symlinks
