This repository contains a background process to work with a custom mail server in order to implement "masked emails".

# Overview

The background process listens on an Azure Storage Queue for commands that are then executed locally.
The commands are implemented in terms of [PowerShell scripts](https://github.com/springcomp/masked-emails-pwsh) that must be installed separately.

__Warning__: The package available publicly has a connection string to an Azure Storage account that is no longer valid. Please, make sure to update the connection string in the `/opt/masked-emails/daemon/appsettings.json` file.

# How-to Install

```
~# wget https://masked.blob.core.windows.net/debian/masked-emails-daemon.deb
~# dpkg -i masked-emails-daemon.deb
```

The daemon attemps to write to `syslog` using UDP.
You need to enable UDP communication in `/etc/rsyslog.conf`:

```
# provides UDP syslog reception
module(load="imudp")
input(type="imudp" port="514")
```


