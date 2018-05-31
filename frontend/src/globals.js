import Secret from './secret'

class Globals
{
  static getBackendUrl() {
    if (process.env.NODE_ENV !== 'production') {
      return "http://api.pharmacy-finder.machonacho.io/api/"
    } else {
      return "http://localhost:5000/api/"
    }
  }

  static getGoogleAPIUrl() {
    return "http://maps.googleapis.com/maps/api/js"
  }

  static getGoogleKey() {
    return Secret.getGoogleKey()
  }

  static getCompleteGoogleAPIUrl() {
    return this.getGoogleAPIUrl() + "?key=" + this.getGoogleKey()
  }
}

export default Globals
