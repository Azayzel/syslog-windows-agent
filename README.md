# Jakkl - Syslog Windows Agent ![Jakkl](https://github.com/Azayzel/syslog-windows-agent/blob/master/jakkl_sm.png "Jakkl")

A Free Windows Event Collector Agent to send logs to a Syslog (ex: Syslog-ng) Server. The agent uses the standard [SYSLOG](https://tools.ietf.org/html/rfc5424) protocol for sending messages.

# Getting Started 

Download the built agent from <a href="https://github.com/Azayzel/syslog-windows-agent/blob/master/Build/Jakkl.zip">Here</a>, or clone this repo and build yourself.

## Configure Jakkl

<table>
  <tr>
    <td> <image src="https://github.com/Azayzel/syslog-windows-agent/blob/master/jakkl_syslog_windows.png"/> </td>
    <td>
		<ul>
			<li>Set program startup</li> 
			<li>Set event sources and types to monitor</li> 
			<li>Set Syslog Server IP/FQDN and Port</li>
		</ul>
	</td>
  </tr>
  <tr>
    <td colspan="2">**All Settings are saved to the registry**</td>
  </tr>
</table>



# Agent In Action
Coming Soon (GIF of messages from windows to Syslog Server)

# To-Dos
- [x] Push Code to GitHub
- [ ] Allow MultiSelect of Event Sources
- [ ] Allow MultiSelect of Event Types

