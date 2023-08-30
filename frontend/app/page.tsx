// pages/index.tsx
'use client';
import { AuctionListComponent } from '@/components/auction-list';
import { AuctionItem } from '@/models/auction-item';
import React, { useState } from 'react';

const HomePage: React.FC = () => {
  const [auctionItems, setAuctionItems] = useState<AuctionItem[]>([
    {
      id: 1,
      nummer: 'A123',
      model: 'Laptop',
      seriennummer: 'S12345',
      prices: '100'
    },
    {
      id: 2,
      nummer: 'A124',
      model: 'Desktop Computer',
      seriennummer: 'S67890',
      prices: '200'
    }
    // Add more items here...
  ]);

  return (
    <div className="container mx-auto p-4">
      <h1 className="mb-4 text-2xl font-semibold">Diamant Verteigerungs Platform</h1>
      <AuctionListComponent items={auctionItems} />
    </div>
  );
};

export default HomePage;
