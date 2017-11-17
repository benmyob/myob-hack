import urllib.request
import json

class RestClient:

    base_url = 'http://playmusic.azurewebsites.net/play-music/api/'

    def get_all_users(self):
        return self.get_rest_response('users')

    def get_user(self, mac_addr):
        return self.get_rest_response('users/' + mac_addr)

    def get_registered_macs(self):
        return self.get_rest_response('registered-mac-addresses')

    def get_rest_response(self, path):
        url = self.base_url + path
        with urllib.request.urlopen(url) as f:
            str_response = f.read().decode('utf-8')
            return json.loads(str_response)
