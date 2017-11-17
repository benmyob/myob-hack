from __future__ import unicode_literals
import os
import time
import spotipy
import spotify
import threading
from spotipy.oauth2 import SpotifyClientCredentials


class HackClient:
    sp = None
    username = ""
    password = ""

    loop = None
    session = None
    audioassigned = False
    audio = None

    def __init__(self):
        self.sp = HackClient.auth()
        self.username = os.getenv("PYSPOTIFY_USERNAME")
        self.password = os.getenv("PYSPOTIFY_PASSWORD")

    @staticmethod
    def auth():
        client_credentials_manager = SpotifyClientCredentials()
        return spotipy.Spotify(client_credentials_manager=client_credentials_manager)

    def find(self, search_str):
        return self.sp.search(search_str)

    def findartist(self, artistname):
        return self.sp.search(q='artist:' + artistname, type='artist')

    def findsongbyartist(self, artistname, songname):
        return self.sp.search(q='artist:' + artistname + ' track:' + songname, type='track')

    def getpreviewforsongbyartist(self, artistname, songname):
        result = self.findsongbyartist(artistname, songname)
        tracks = result["tracks"]["items"]
        previewurls = [a["preview_url"] for a in tracks]
        if len(previewurls) > 0:
            return previewurls[0]

        return None

    def gettrackuriforsongbyartist(self, artistname, songname):
        result = self.findsongbyartist(artistname, songname)
        tracks = result["tracks"]["items"]
        trackuris = [a["uri"] for a in tracks]
        if len(trackuris) > 0:
            return trackuris[0]

        return None

    def findsong(self, songname):
            return self.sp.search(q='track:' + songname, type='track')

    def stop(self):
        if self.loop is not None:
            self.session.player.unload()

    def loadandplay(self, track_uri):
        # Play a track
        track = self.session.get_track(track_uri).load()
        self.session.player.load(track)
        self.session.player.play()    

    def playtrack(self, track_uri):
        # Assuming a spotify_appkey.key in the current dir
        if self.session is None:
            self.session = spotify.Session()


        # Process events in the background
        self.loop = spotify.EventLoop(self.session)
        self.loop.start()

        # Connect an audio sink
        if self.audio is None:
        	self.audio = spotify.AlsaSink(self.session)
        else:
                self.loadandplay(track_uri)
                return
        # Events for coordination
        logged_in = threading.Event()
        end_of_track = threading.Event()

        def on_connection_state_updated(session):
            if session.connection.state is spotify.ConnectionState.LOGGED_IN:
                logged_in.set()

        def on_end_of_track(self):
            end_of_track.set()

        # Register event listeners
        self.session.on(
            spotify.SessionEvent.CONNECTION_STATE_UPDATED, on_connection_state_updated)
        self.session.on(spotify.SessionEvent.END_OF_TRACK, on_end_of_track)

        # Assuming a previous login with remember_me=True and a proper logout
        self.session.login(self.username, self.password)

        logged_in.wait()
        self.loadandplay(track_uri)
