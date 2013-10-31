using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace BluetoothAppToDeviceDemo2
{
    public class CarControl
    {
        public enum DirectionValues
        {
            stop = 0x00,

            forward = 0x10,
            forwardLeft = 0x50,
            forwardRight = 0x60,

            backwards = 0x20,
            backwardsLeft = 0x70,
            backwardsRight = 0x80,
        }

        private StreamSocket _socket = null;
        private DataWriter _dataWriter = null;

        private byte _speed = 0;
        private byte _direction = 0;

        public CarControl(StreamSocket socket)
        {
            _socket = socket;
            _dataWriter = new DataWriter(socket.OutputStream);
        }

        public async void SendCommand()
        {
            _dataWriter.WriteByte((byte)(_direction + _speed));
            await _dataWriter.StoreAsync();
        }


        #region Properties

        public DirectionValues Direction
        {
            get
            {
                return (DirectionValues)_direction;
            }
            set
            {
                _direction = (byte)value;
                SendCommand();
            }
        }

        public byte Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                if (value > 15) throw new ArgumentException("Speed must be between 0 and 15");
                _speed = value;
                SendCommand();
            }
        }

        #endregion //Properties

    }
}
