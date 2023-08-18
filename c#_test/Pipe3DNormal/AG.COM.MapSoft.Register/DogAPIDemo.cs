using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using SuperDog;

namespace AG.COM.MapSoft.Register
{
    public class DogDemo
    {
        protected StringCollection stringCollection;

        public const string defaultScope = "<dogscope />";

        
        private string scope;

        private Dog m_dog = null;

        /// <summary>
        /// Constructor
        /// Intializes the object.
        /// </summary>
        public DogDemo()
        {

            // next could be considered ugly.
            // build up a string collection holding
            // the status codes in a human readable manner.
            string[] stringRange = new string[] 
            {
                "Request successfully completed.", 
                "Request exceeds data file range.",
                "",
                "System is out of memory.", 
                "Too many open login sessions.", 
                "Access denied.",
                "",
                "Required SuperDog not found.", 
                "Encryption/decryption data length is too short.", 
                "Invalid input handle.", 
                "Specified File ID not recognized by API.", 
                "",
                "",
                "",
                "",
                "Invalid XML format.",
                "",
                "",
                "SuperDog to be updated not found.", 
                "Required XML tags not found; Contents in binary data are missing or invalid.", 
                "Update not supported by SuperDog.", 
                "Update counter is set incorrectly.", 
                "Invalid Vendor Code passed.",
                "",
                "Passed time value is outside supported value range.", 
                "",
                "Acknowledge data requested by the update, however the ack_data input parameter is NULL.", 
                "Program running on a terminal server.", 
                "",
                "Unknown algorithm used in V2C file.", 
                "Signature verification failed.", 
                "Requested Feature not available.", 
                "",
                "Communication error between API and local SuperDog License Manager.",
                "Vendor Code not recognized by API.", 
                "Invalid XML specification.", 
                "Invalid XML scope.", 
                "Too many SuperDog currently connected.", 
                "",
                "Session was interrupted.", 
                "",
                "Feature has expired.", 
                "SuperDog License Manager version too old.",
                "USB error occurred when communicating with a SuperDog.", 
                "",
                "System time has been tampered.", 
                "Communication error occurred in secure channel.", 
                "",
                "",
                "",
                "Unable to locate a Feature matching the scope.", 
                "",
                "",
                "",
                "Trying to install a V2C file with an update counter that is out" + 
                "of sequence with the update counter in the SuperDog." + 
                "The values of the update counter in the file are lower than" + 
                "those in the SuperDog.", 
                "Trying to install a V2C file with an update counter that is out" + 
                "of sequence with the update counter in the SuperDog." + 
                "The first value of the update counter in the file is greater than" + 
                "the value in the SuperDog."
            };

            stringCollection = new StringCollection();
            stringCollection.AddRange(stringRange);

            for (int n = stringCollection.Count; n < 400; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringRange = new string[]  
            {
                "A required API dynamic library was not found.",
                "The found and assigned API dynamic library could not be verified.",
            };

            stringCollection.AddRange(stringRange);

            for (int n = stringCollection.Count; n < 500; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringRange = new string[]  
            {
                "Object incorrectly initialized.",
                "A parameter is invalid.",
                "Already logged in.",
                "Already logged out."
            };

            stringCollection.AddRange(stringRange);

            for (int n = stringCollection.Count; n < 525; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringCollection.Insert(525, "Incorrect use of system or platform.");

            for (int n = stringCollection.Count; n < 698; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringCollection.Insert(698, "Capability is not available.");
            stringCollection.Insert(699, "Internal API error.");
        }


        /// <summary>
        /// Demonstrates how to perform a login and an automatic
        /// logout using C#'s scope clause.
        /// </summary>
        protected void LoginDefaultAutoDemo()
        {

            DogFeature feature = DogFeature.Default;

            // this will perform a logout and object disposal
            // when the using scope is left.
            using (Dog dog = new Dog(feature))
            {
                DogStatus status = dog.Login(DogVendorCode.Code, scope);
                ReportStatus(status);

            }
        }

        /// <summary>
        /// Demonstrates how to login into a dog using the
        /// default feature. The default feature is ALWAYS 
        /// available in every SuperDog.
        /// </summary>
        protected Dog LoginDefaultDemo()
        {
            DogFeature feature = DogFeature.Default;

            Dog dog = new Dog(feature);

            DogStatus status = dog.Login(DogVendorCode.Code, scope);
            ReportStatus(status);

            // Please note that there is no need to call
            // a logout function explicitly - although it is
            // recommended. The garbage collector will perform
            // the logout when disposing the object.
            // If you need more control over the logout procedure
            // perform one of the more advanced tasks.
            return dog.IsLoggedIn() ? dog : null;
        }


        /// <summary>
        /// Demonstrates how to login using a feature id.
        /// </summary>
        protected Dog LoginDemo(DogFeature feature)
        {
            // create a dog object using a feature
            // and perform a login using the vendor code.
            Dog dog = new Dog(feature);

            DogStatus status = dog.Login(DogVendorCode.Code, scope);
            ReportStatus(status);

            return dog.IsLoggedIn() ? dog : null;
        }


        /// <summary>
        /// Demonstrates how to perform a login using the default
        /// feature and how to perform an automatic logout
        /// using the SuperDog's Dispose method.
        /// </summary>
        protected void LoginDisposeDemo()
        {
            DogFeature feature = DogFeature.Default;

            Dog dog = new Dog(feature);

            DogStatus status = dog.Login(DogVendorCode.Code, scope);
            ReportStatus(status);

            dog.Dispose();
        }


        /// <summary>
        /// Demonstrates how to perform a login and a logout
        /// using the default feature.
        /// </summary>
        protected void LoginLogoutDefaultDemo()
        {
            DogFeature feature = DogFeature.Default;

            Dog dog = new Dog(feature);

            DogStatus status = dog.Login(DogVendorCode.Code, scope);
            ReportStatus(status);

            if (DogStatus.StatusOk == status)
            {
                status = dog.Logout();

                ReportStatus(status);
            }

            // recommended, but not mandatory
            // this call ensures that all resources to SuperDog
            // are freed immediately.
            dog.Dispose();
        }


        /// <summary>
        /// Performs a logout operation
        /// </summary>
        protected void LogoutDemo(ref Dog dog)
        {
            if ((null == dog) || !dog.IsLoggedIn())
                return;

            DogStatus status = dog.Logout();

            // get rid of the dog immediately.
            dog.Dispose();
            dog = null;
        }

        /// <summary>
        /// Dumps an operation status into the 
        /// referenced TextBox.
        /// </summary>
        protected void ReportStatus(DogStatus status)
        {
            if (DogStatus.StatusOk == status)
            {
                //
            }
            else
            {
                throw new Exception(
                    string.Format("     Error: {0} (DogStatus::{1})\r\n",
                                        stringCollection[(int)status],
                                        status.ToString()));
            }
        }

        /// <summary>
        /// Demonstrates how to get current time and date of 
        /// a SuperDog when available.
        /// </summary>
        protected void TestTimeDemo(Dog dog)
        {
            if ((null == dog) || !dog.IsLoggedIn())
                return;

            DateTime time = DateTime.Now;
            DogStatus status = dog.GetTime(ref time);
            ReportStatus(status);
        }


        /// <summary>
        /// Runs the API demo.
        /// </summary>
        public bool RunDemo(string scope, ref string ExMessage)
        {
            try
            {
                this.scope = scope;

                // run the API demo using the default feature
                // (ALWAYS present in every SuperDog)
                Dog dog = LoginDefaultDemo();
                LogoutDemo(ref dog);

                dog = LoginDemo(new DogFeature(DogFeature.FromFeature(1).Feature));
                //TestTimeDemo(dog);
                m_dog = dog;
            }
            catch (Exception ex)
            {
                ExMessage = ex.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获得软件狗对应软件的名称
        /// 注意写狗的时候 需要新增ID = 1的数据文件，文件内容是地方版本的名称
        /// </summary>
        /// <returns></returns>
        public string GetDogSoftName(ref string ExMessage)
        {
            try
            {
                if(m_dog == null)
                    return "";
                DogFile file = m_dog.GetFile(1); 
                if(file == null)
                    return "";
                int nLen = 0;
                file.FileSize(ref nLen);
                if(nLen <= 0)
                    return "";
                byte[] data = new byte[nLen];
                file.Read(data, 0, nLen);
                return System.Text.Encoding.Default.GetString(data);
            }
            catch (Exception ex)
            {
                ExMessage = ex.Message.ToString();
                return "";
            }
        }
    }
}
