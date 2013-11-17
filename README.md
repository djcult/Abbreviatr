Abbreviatr
==========

'GeneratePasswords' - Generate passwords by abbreviating the sentences in a plain text file.

Expected usage: Abbreviatr.exe GeneratePasswords <options>

<options> available:
  -i, --inputFile=VALUE			The plain text input file
  -n, --number=VALUE			Number of passwords; default is do as many as possible
  --mi, --minLength=VALUE		Minimum required password length; default is 6
  --ma, --maxLength=VALUE		Maximum password length
  -c, --char=VALUE				Take nth character from each word (or the last character if n > length); default is 1
  -v, --Verbose					Verbose output