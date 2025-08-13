#!/bin/bash

# Only process staged C# files
FILES=$(git diff --cached --name-only --diff-filter=ACM | grep -E '\.cs$')

for file in $FILES; do
  if [ -f "$file" ]; then
    # Remove lines matching the Token comment
    sed -i.bak '/^\s*\/\/\s*Token:\s*0x[0-9A-Fa-f]\+\(\s\+RID:\s\+[0-9]\+\)\?\s*$/d' "$file"

    # Re-add cleaned file to staging
    git add "$file"

    # Remove backup file
    rm "$file.bak"
  fi
done

exit 0
