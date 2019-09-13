import Secret from './secret'

class Globals
{
  static getBackendUrl() {
    if (process.env.NODE_ENV === 'production') {
      return "http://josh-pharmacybackend-dev.us-east-1.elasticbeanstalk.com/api"
    } else {
      return "//localhost:5000/api"
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
