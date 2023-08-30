import { AuctionItem } from './auction-item';

export interface DeviceGroup {
  id: string;
  name: string;
  devices: AuctionItem[];
}
