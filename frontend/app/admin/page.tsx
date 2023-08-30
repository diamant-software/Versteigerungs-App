'use client';
import React, { FormEvent, useState } from 'react';

const AdminPage = () => {
  const [newDevice, setNewDevice] = useState({
    nummer: '',
    model: '',
    seriennummer: '',
    prices: ''
  });

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setNewDevice((prevState) => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    // Hier kannst du den Code einfügen, um das neue Gerät zu speichern, z. B. eine API-Anfrage an deinen Server senden.
    console.log('New Device:', newDevice);
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="mb-4 text-2xl font-semibold">Admin Dashboard</h1>
      <form onSubmit={handleSubmit} className="rounded-lg bg-white p-6 shadow-md">
        <h2 className="mb-2 text-xl font-semibold">Add New Device</h2>
        <input
          type="text"
          name="nummer"
          value={newDevice.nummer}
          onChange={handleInputChange}
          placeholder="Auction Number"
          className="mb-2 border p-2"
        />
        <input
          type="text"
          name="model"
          value={newDevice.model}
          onChange={handleInputChange}
          placeholder="Model"
          className="mb-2 border p-2"
        />
        <input
          type="text"
          name="seriennummer"
          value={newDevice.seriennummer}
          onChange={handleInputChange}
          placeholder="Serial Number"
          className="mb-2 border p-2"
        />
        <input
          type="text"
          name="prices"
          value={newDevice.prices}
          onChange={handleInputChange}
          placeholder="Price"
          className="mb-2 border p-2"
        />
        <button type="submit" className="mt-2 rounded bg-blue-500 px-4 py-2 text-white">
          Add Device
        </button>
      </form>
    </div>
  );
};

export default AdminPage;
